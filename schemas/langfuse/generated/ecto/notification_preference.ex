defmodule Langfuse.NotificationPreference do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          channel: atom(),
          type: atom(),
          enabled: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Langfuse.User.t() | nil,
          project: Langfuse.Project.t() | nil
        }

  schema "notification_preferences" do
    field(:id, :string, primary_key: true)
    field(:channel, Ecto.Enum, values: [:EMAIL])
    field(:type, Ecto.Enum, values: [:COMMENT_MENTION])
    field(:enabled, :boolean, default: true)
    belongs_to(:user, Langfuse.User, foreign_key: :user_id, type: :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end