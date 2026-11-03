defmodule Formbricks.Workspace do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          legacy_environment_id: String.t() | nil,
          styling: map(),
          config: map(),
          recontact_days: integer(),
          link_survey_branding: boolean(),
          in_app_survey_branding: boolean(),
          placement: atom(),
          click_outside_close: boolean(),
          overlay: atom(),
          overlay_color: String.t() | nil,
          overlay_opacity: integer() | nil,
          logo: map() | nil,
          app_setup_completed: boolean(),
          custom_head_scripts: String.t() | nil,
          custom_css: map() | nil,
          custom_css_previous: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Formbricks.Organization.t() | nil,
          languages: [Formbricks.Language.t()],
          embedded_data: [Formbricks.EmbeddedData.t()],
          workspace_teams: [Formbricks.WorkspaceTeam.t()],
          feedback_directory_workspaces: [Formbricks.FeedbackDirectoryWorkspace.t()],
          charts: [Formbricks.Chart.t()],
          dashboards: [Formbricks.Dashboard.t()],
          creatives: [Formbricks.Creative.t()],
          surveys: [Formbricks.Survey.t()],
          contacts: [Formbricks.Contact.t()],
          action_classes: [Formbricks.ActionClass.t()],
          contact_attribute_keys: [Formbricks.ContactAttributeKey.t()],
          webhooks: [Formbricks.Webhook.t()],
          tags: [Formbricks.Tag.t()],
          segments: [Formbricks.Segment.t()],
          integrations: [Formbricks.Integration.t()],
          api_key_workspaces: [Formbricks.ApiKeyWorkspace.t()],
          feedback_sources: [Formbricks.FeedbackSource.t()],
          workflows: [Formbricks.Workflow.t()],
          workflow_runs: [Formbricks.WorkflowRun.t()]
        }

  schema "Workspace" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:legacy_environment_id, :string, source: :legacyEnvironmentId)
    field(:styling, :map, default: %{"allowStyleOverwrite" => true})
    field(:config, :map, default: %{})
    field(:recontact_days, :integer, default: 7, source: :recontactDays)
    field(:link_survey_branding, :boolean, default: true, source: :linkSurveyBranding)
    field(:in_app_survey_branding, :boolean, default: true, source: :inAppSurveyBranding)
    field(:placement, Ecto.Enum, values: [:bottomLeft, :bottomRight, :topLeft, :topRight, :center], default: :bottomRight)
    field(:click_outside_close, :boolean, default: true, source: :clickOutsideClose)
    field(:overlay, Ecto.Enum, values: [:none, :light, :dark], default: :none)
    field(:overlay_color, :string, source: :overlayColor)
    field(:overlay_opacity, :integer, source: :overlayOpacity)
    field(:logo, :map)
    field(:app_setup_completed, :boolean, default: false, source: :appSetupCompleted)
    field(:custom_head_scripts, :string, source: :customHeadScripts)
    field(:custom_css, :map, source: :customCss)
    field(:custom_css_previous, :map, source: :customCssPrevious)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, Formbricks.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    has_many(:languages, Formbricks.Language, foreign_key: :workspace_id)
    has_many(:embedded_data, Formbricks.EmbeddedData, foreign_key: :workspace_id)
    has_many(:workspace_teams, Formbricks.WorkspaceTeam, foreign_key: :workspace_id)
    has_many(:feedback_directory_workspaces, Formbricks.FeedbackDirectoryWorkspace, foreign_key: :workspace_id)
    has_many(:charts, Formbricks.Chart, foreign_key: :workspace_id)
    has_many(:dashboards, Formbricks.Dashboard, foreign_key: :workspace_id)
    has_many(:creatives, Formbricks.Creative, foreign_key: :workspace_id)
    has_many(:surveys, Formbricks.Survey, foreign_key: :workspace_id)
    has_many(:contacts, Formbricks.Contact, foreign_key: :workspace_id)
    has_many(:action_classes, Formbricks.ActionClass, foreign_key: :workspace_id)
    has_many(:contact_attribute_keys, Formbricks.ContactAttributeKey, foreign_key: :workspace_id)
    has_many(:webhooks, Formbricks.Webhook, foreign_key: :workspace_id)
    has_many(:tags, Formbricks.Tag, foreign_key: :workspace_id)
    has_many(:segments, Formbricks.Segment, foreign_key: :workspace_id)
    has_many(:integrations, Formbricks.Integration, foreign_key: :workspace_id)
    has_many(:api_key_workspaces, Formbricks.ApiKeyWorkspace, foreign_key: :workspace_id)
    has_many(:feedback_sources, Formbricks.FeedbackSource, foreign_key: :workspace_id)
    has_many(:workflows, Formbricks.Workflow, foreign_key: :workspace_id)
    has_many(:workflow_runs, Formbricks.WorkflowRun, foreign_key: :workspace_id)
    timestamps(inserted_at_source: :created_at)
  end
end