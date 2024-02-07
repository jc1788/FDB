function PageRedir(url) {
if (top == self) {
parent.location = url;
}
else {
parent.frames['redir_frame'].location = url;
}
}
