package pt.ipp.estg.dockin

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.enableEdgeToEdge
import dagger.hilt.android.AndroidEntryPoint
import pt.ipp.estg.dockin.ui.navigation.AppNavGraph
import pt.ipp.estg.dockin.ui.theme.DockInTheme

@AndroidEntryPoint
class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContent {
            DockInTheme {
                AppNavGraph()
            }
        }
    }
}
