defmodule Trigger.ProjectAlertChannel do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          deduplication_key: String.t(),
          user_provided_deduplication_key: boolean(),
          enabled: boolean(),
          type: atom(),
          name: String.t(),
          properties: map(),
          alert_types: atom(),
          environment_types: atom(),
          error_alert_config: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          integration: Trigger.OrganizationIntegration.t() | nil,
          project: Trigger.Project.t() | nil,
          alerts: [Trigger.ProjectAlert.t()],
          alert_storages: [Trigger.ProjectAlertStorage.t()]
        }

  schema "ProjectAlertChannel" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string, source: :friendlyId)
    field(:deduplication_key, :string, source: :deduplicationKey)
    field(:user_provided_deduplication_key, :boolean, default: false, source: :userProvidedDeduplicationKey)
    field(:enabled, :boolean, default: true)
    field(:type, Ecto.Enum, values: [:EMAIL, :SLACK, :WEBHOOK])
    field(:name, :string)
    field(:properties, :map)
    field(:alert_types, {:array, Ecto.Enum}, values: [:TASK_RUN, :TASK_RUN_ATTEMPT, :DEPLOYMENT_FAILURE, :DEPLOYMENT_SUCCESS, :ERROR_GROUP, :DASHBOARD_AGENT_WATCH], source: :alertTypes)
    field(:environment_types, {:array, Ecto.Enum}, values: [:PRODUCTION, :STAGING, :DEVELOPMENT, :PREVIEW], source: :environmentTypes)
    field(:error_alert_config, :map, source: :errorAlertConfig)
    field(:integration_id, :string, source: :integrationId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:integration, Trigger.OrganizationIntegration, foreign_key: :integration_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_many(:alerts, Trigger.ProjectAlert, foreign_key: :channel_id)
    has_many(:alert_storages, Trigger.ProjectAlertStorage, foreign_key: :alert_channel_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end