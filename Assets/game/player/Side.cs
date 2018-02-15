public class Side {
    private string _left = "left";
    private string _right = "right";
    private string _currentSide = "left";

    public Side(string startSide) {
        if ( startSide == _left || startSide == _right ) {
            _currentSide = startSide;
        }
    }
    public string side {
        get {
            return _currentSide;
        }
    }

    public string left {
        get {
            return _left;
        }
    }
    public string right {
        get {
            return _right;
        }
    }

    public void flip() {
        _currentSide = _currentSide == _left ? _right : _left;
    }
    
}