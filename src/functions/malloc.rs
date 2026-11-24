use std::os::raw::c_int;

const M_TRIM_THRESHOLD: c_int = -2;
const M_MMAP_THRESHOLD: c_int = -3;
const M_ARENA_MAX: c_int = -4;

unsafe extern "C" {
    fn mallopt(param: c_int, value: c_int) -> c_int;
}

/// Evita que a glibc acumule memória por fragmentação (útil em hosts com RAM limitada).
/// Chame uma única vez, antes de qualquer outra alocação relevante.
pub fn configure_malloc() {
    // SAFETY: `mallopt` é thread-safe e só ajusta parâmetros internos do alocador.
    unsafe {
        mallopt(M_ARENA_MAX, 1);
        mallopt(M_MMAP_THRESHOLD, 131072);
        mallopt(M_TRIM_THRESHOLD, 65536);
    }
}
