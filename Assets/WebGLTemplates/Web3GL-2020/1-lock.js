// (A) LOCK SCREEN ORIENTATION
function lock (orientation) {
  // (A1) GO INTO FULL SCREEN FIRST
  let de = document.documentElement;
  if (de.requestFullscreen) { de.requestFullscreen(); }
  else if (de.mozRequestFullScreen) { de.mozRequestFullScreen(); }
  else if (de.webkitRequestFullscreen) { de.webkitRequestFullscreen(); }
  else if (de.msRequestFullscreen) { de.msRequestFullscreen(); }

  // (A2) THEN LOCK ORIENTATION
  screen.orientation.lock(orientation);
}

// (B) UNLOCK SCREEN ORIENTATION
function unlock () {
  // (B1) UNLOCK FIRST
  screen.orientation.unlock();

  // (B2) THEN EXIT FULL SCREEN
  if (document.exitFullscreen) { document.exitFullscreen(); }
  else if (document.webkitExitFullscreen) { document.webkitExitFullscreen(); }
  else if (document.mozCancelFullScreen) { document.mozCancelFullScreen(); }
  else if (document.msExitFullscreen) { document.msExitFullscreen(); }
}