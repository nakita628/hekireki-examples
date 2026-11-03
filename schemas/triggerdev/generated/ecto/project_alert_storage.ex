defmodule Trigger.ProjectAlertStorage do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          alert_type: atom(),
          storage_id: String.t(),
          storage_data: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Trigger.Project.t() | nil,
          alert_channel: Trigger.ProjectAlertChannel.t() | nil
        }

  schema "ProjectAlertStorage" do
    field(:id, :string, primary_key: true)
    field(:alert_type, Ecto.Enum, values: [:TASK_RUN, :TASK_RUN_ATTEMPT, :DEPLOYMENT_FAILURE, :DEPLOYMENT_SUCCESS, :ERROR_GROUP, :DASHBOARD_AGENT_WATCH], source: :alertType)
    field(:storage_id, :string, source: :storageId)
    field(:storage_data, :map, source: :storageData)
    field(:project_id, :string, source: :projectId)
    field(:alert_channel_id, :string, source: :alertChannelId)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:alert_channel, Trigger.ProjectAlertChannel, foreign_key: :alert_channel_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end