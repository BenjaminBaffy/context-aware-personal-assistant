import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { RootState } from '../../../store/store'
import { rasaActions } from '../../rasa/rasaSlice'
import useRasa from '../../rasa/useRasa'
import { demoActions } from '../demoSlice'

import styles from './DemoComponent.module.scss'


// SHOULD BE OUTSOURCED

interface LinkProps {
    to: string,
    children: React.ReactElement | React.ReactElement[] | string
}

const Link = ({ to, children }: LinkProps) => <a className={styles.link} href={to}>{children}</a>

// ---



// SHOULD BE OUTSOURCED

interface TopicProps {
    title: string,
    description?: string,
    linkTo?: string,
    children?: any
}

const Topic = ({ title, description, linkTo, children }: TopicProps) => (
    <>
        <h2><Link to={linkTo || ""}>{title}</Link></h2>
        <div className={styles.topicDescription}>
            {description && <pre>{description}</pre>}
            {children}
        </div>
    </>
)

// ---



const DemoComponent = () => {
    const dispatch = useDispatch()
    const { demoStateField } = useSelector((state: RootState) => state.demo)


    const { response, submitCommand } = useRasa()

    const [inputValue, setInputValue] = useState<string>('')
    const [botTextValue, setBotTextValue] = useState<string>('')
    const [botTextAreaValue, setBotTextAreaValue] = useState<string>('')

    const handleInputChange = (e: any) => {
        const { value } = e.target
        setInputValue(value)
    }

    const handleBotInputChange = (e: any) => {
        const { value } = e.target
        setBotTextValue(value)
    }

    const handleFormSubmit = (e: any) => {
        e.preventDefault() // prevent page reloading
        dispatch(demoActions.setDemoStateField(inputValue))
    }

    const handleEmptyInputChange = (e: any) => {
        e.preventDefault();
    }

    const handleBotFormSubmit = (e: any) => {
        e.preventDefault()
        const message = e.target.botText.value
        submitCommand(message)
    }

    useEffect(() => {
        setBotTextAreaValue(prev => prev + `Rasa: ${response}\n`);
    }, [response])

    return (
        <div className={styles.demoStyle}>
            <h1>Demo Component ⚙️</h1>
            {/* inline style (not recommended, always use css modules) */}
            <pre style={{ color: 'black' }}>Hello, World!</pre>

            <Topic
                title="Environment variables"
                description={process.env.REACT_APP_DEMO_ENV_VAR}
            />

            <Topic
                title="CSS/Sass modules"
                description="Add a file named 'ComponentName.module.scss' to it's folder"
                linkTo="https://create-react-app.dev/docs/adding-a-css-modules-stylesheet"
            />

            <Topic
                title="React Hooks"
                linkTo="https://reactjs.org/docs/hooks-intro.html"
            >
                <p>Always starts with <strong>use</strong></p>
                <pre>use_______</pre>
            </Topic>

            <Topic
                title="Redux Toolkit (Creating Slices)"
                linkTo="https://redux-toolkit.js.org/tutorials/quick-start"
            >
                <div>This is a demo state: <strong>{demoStateField}</strong></div>
                <form onSubmit={handleFormSubmit}>
                    <input value={inputValue} onChange={handleInputChange} />
                    <button type="submit">Set demo state field</button>
                </form>
                <h4>Terms</h4>
                <pre>State</pre>
                <pre>Reducer (Action) - updates state</pre>
                <pre>Thunk (async action) - async action, side effects</pre>
                <pre>Dispatch - invokes action</pre>
                <pre>Slice - contains slice name, initial state, reducers</pre>
                <h4>Info</h4>
                <p>
                    Redux is based on the Flux pattern. Used for strictly managing and updating state called <strong>Store</strong>.
                    The state cannot be directly modified, rather an <strong>Action</strong> is <strong>Dispatched</strong> that is handled by a <strong>Reducer</strong> to determine how to update the current state.
                    So this ensures that a particular part (<strong>Slice</strong>) of the state is only updated in that function.
                    Moreover, the store is globally injected/provided at the root of the application.
                </p>
                <h4>Access from React Components</h4>
                <pre>const dispatch = useDispatch()</pre>
                <pre>const { "{ field }" } = useSelector((state: RootState) ={'>'} state.sliceName)</pre>
            </Topic>

            <Topic
                title="Folder structure"
            >
                <pre>components/ - common, general, reusable components </pre>
                <pre>features/ - MOST IMPORTANT. The features of the application, these will contain the slices (i.e. login/, admin/, payment/, etc.)</pre>
                <pre>hooks/ - reusable hook definitions, side-effect logics, function extractions</pre>
                <pre>models/ - data types, enums, interfaces</pre>
                <pre>routes/ - routing</pre>
                <pre>services/ - api services</pre>
                <pre>store/ - redux store</pre>
                <pre>utils/ - utility functions</pre>
                <p>Every component should be encapsulated in it's own folder, that has the name of the component.</p>
                <p>Each encapsulated component folder should contain the following files:</p>
                <pre>ComponentName.tsx</pre>
                <pre>ComponentName.module.scss</pre>
                <p>When populating the <strong>features/</strong> folders, add the <strong>featureNameSlice.ts</strong> files. They're redux slices used to chop up the state of the application. <strong>Can be arbitrary deeply nested!</strong> They're a matter of reducer exporting and combining.</p>
            </Topic>

            <Topic title="Talk to The Bot">
                <form onSubmit={handleBotFormSubmit}>
                    <input name="botText" value={botTextValue} onChange={handleBotInputChange} />
                    <button type="submit">Send message</button>
                </form>
                <textarea name="botResponses" value={botTextAreaValue} rows={20} cols={200} onChange={handleEmptyInputChange} />
            </Topic>
        </div>
    )
}

export default DemoComponent
