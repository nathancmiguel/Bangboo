use crate::constants::GUILD;
use crate::discord::*;
use crate::functions::{error, global_boost, global_message};
use std::error::Error;
use twilight_gateway::{Event, EventType};
use twilight_http::request::guild::user;
use twilight_model::id::marker::{ChannelMarker, RoleMarker};
use twilight_model::id::Id;

pub struct MemberUpdate;

#[async_trait]
impl EventHandler for MemberUpdate {
    fn event(&self) -> EventType {
        EventType::MemberUpdate
    }

    async fn run(&self, ctx: Context, event: Event) -> Result<(), Box<dyn Error + Send + Sync>> {
        let member_update = match event {
            Event::MemberUpdate(e) => e,
            _ => return Ok(()),
        };

        let guild_id = &member_update.guild_id;
        let user_id = &member_update.user.id;
        //let channel_id = Id::<ChannelMarker>::new(GUILD.channels.announcement);
        let user = &member_update.user;

        if user.bot {
            return Ok(());
        }

        let booster_role = Id::<RoleMarker>::new(GUILD.roles.boosters);
        if member_update.premium_since.is_some() {
            ctx.http
                .add_guild_member_role(guild_id.clone(), user_id.clone(), booster_role)
                .await?;

            let mut system_channel_id = None;

            if let Some(cached_guild) = ctx.cache.guild(guild_id.clone()) {
                if let Some(sys_channel_id) = cached_guild.system_channel_id() {
                    system_channel_id = Some(sys_channel_id);
                }
            }

            if system_channel_id.is_none() {
                if let Ok(guild) = ctx.http.guild(guild_id.clone()).await?.model().await {
                    if let Some(sys_channel_id) = guild.system_channel_id {
                        system_channel_id = Some(sys_channel_id);
                    }
                }
            }

            if system_channel_id.is_none() {
                error("System channel not set for guild.");
                return Ok(());
            }

            let system_channel_id = system_channel_id.unwrap();

            global_message(
                &ctx,
                &system_channel_id,
                EventType::MemberUpdate,
                None,
                user,
            )
            .await;

        } else {
            ctx.http
                .remove_guild_member_role(guild_id.clone(), user_id.clone(), booster_role)
                .await?;
        }

        Ok(())
    }
}
