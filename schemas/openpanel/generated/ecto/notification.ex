defmodule OpenPanel.Notification do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          title: String.t(),
          message: String.t(),
          is_read_at: DateTime.t() | nil,
          send_to_app: boolean(),
          send_to_email: boolean(),
          payload: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: OpenPanel.Project.t() | nil,
          integration: OpenPanel.Integration.t() | nil,
          notification_rule: OpenPanel.NotificationRule.t() | nil
        }

  schema "notifications" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:title, :string)
    field(:message, :string)
    field(:is_read_at, OpenPanel.PrismaDateTime, source: :isReadAt)
    field(:send_to_app, :boolean, default: false, source: :sendToApp)
    field(:send_to_email, :boolean, default: false, source: :sendToEmail)
    field(:payload, :map)
    field(:project_id, :string, source: :projectId)
    field(:integration_id, :string, source: :integrationId)
    field(:notification_rule_id, :string, source: :notificationRuleId)
    belongs_to(:project, OpenPanel.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:integration, OpenPanel.Integration, foreign_key: :integration_id, define_field: false, type: :string)
    belongs_to(:notification_rule, OpenPanel.NotificationRule, foreign_key: :notification_rule_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end