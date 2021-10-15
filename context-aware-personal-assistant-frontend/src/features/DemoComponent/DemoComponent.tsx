import React from 'react'

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
    return (
        <div className={styles.demoStyle}>
            <h1>Demo Component ⚙️</h1>
            <pre>Hello, World!</pre>

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
                title="Redux Toolkit (Creating Slices)"
                linkTo="https://redux-toolkit.js.org/tutorials/quick-start"
            />

            <Topic
                title="Folder structure"
            >
                <pre>components/ - common, general, reusable components </pre>
                <pre>features/ - MOST IMPORTANT. The features of the application, these will contain the slices (i.e. login/, admin/, payment/, etc.)</pre>
                <Link to="https://reactjs.org/docs/hooks-intro.html"><pre>hooks/ - reusable hook definitions, side-effect logics, function extractions (link)</pre></Link>
                <pre>models/ - data types, enums, interfaces</pre>
                <pre>routes/ - routing</pre>
                <pre>services/ - api services</pre>
                <pre>store/ - redux store</pre>
                <pre>utils/ - utility functions</pre>
                <p>Every component should be encapsulated in it's own folder, that has the name of the component.</p>
                <p>Each encapsulated component folder should contain the following files:</p>
                <pre>ComponentName.tsx</pre>
                <pre>ComponentName.module.scss</pre>
                <p>When populating the <strong>features/</strong> folders, add the <strong>featureNameSlice.ts</strong> files. They're redux slices used to chop up the state of the application.</p>
            </Topic>
        </div>
    )
}

export default DemoComponent
