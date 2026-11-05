defmodule OpenPanel.NotificationRule do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          send_to_app: boolean(),
          send_to_email: boolean(),
          config: map(),
          template: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: OpenPanel.Project.t() | nil,
          notifications: [OpenPanel.Notification.t()],
          integrations: [OpenPanel.Integration.t()]
        }

  schema "notification_rules" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:name, :string)
    field(:send_to_app, :boolean, default: false, source: :sendToApp)
    field(:send_to_email, :boolean, default: false, source: :sendToEmail)
    field(:config, :map)
    field(:template, :string)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, OpenPanel.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_many(:notifications, OpenPanel.Notification, foreign_key: :notification_rule_id, references: :id)
    many_to_many(:integrations, OpenPanel.Integration, join_through: "_IntegrationToNotificationRule", join_keys: [B: :id, A: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end