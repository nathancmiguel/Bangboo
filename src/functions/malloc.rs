use std::os::raw::c_int;
const M_TRIM_THRESHOLD: c_int = -2;
const M_MMAP_THRESHOLD: c_int = -3;
const M_ARENA_MAX: c_int = -4;

unsafe extern "C" {
    fn malloc_trim(pad: usize);
    fn mallopt(param: c_int, value: c_int) -> c_int;
}

/// Prevents glibc from hoarding memory via memory fragmentation.
pub fn limit_mmap_threshold() {
    unsafe {
        mallopt(M_MMAP_THRESHOLD, 65536);
    }
}

/// Asks glibc to trim malloc arenas.
pub fn trim() {
    unsafe {
        malloc_trim(0);
    }
}

pub fn configure_malloc() {
    unsafe {
        // Set MALLOC_ARENA_MAX to 1
        mallopt(M_ARENA_MAX, 1);

        // Set MALLOC_MMAP_THRESHOLD_ to 131072
        mallopt(M_MMAP_THRESHOLD, 131072);

        // Set MALLOC_TRIM_THRESHOLD_ to 65536
        mallopt(M_TRIM_THRESHOLD, 65536);
    }
}
