defmodule Jitsu.NotificationState do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          workspace_id: String.t(),
          actor_id: String.t(),
          type: String.t(),
          table_name: String.t(),
          channel_id: String.t(),
          flapping_since: DateTime.t() | nil,
          last_notification: DateTime.t(),
          status_change_id: integer(),
          error: String.t() | nil
        }

  schema "NotificationState" do
    field(:workspace_id, :string, source: :workspaceId)
    field(:actor_id, :string, primary_key: true, source: :actorId)
    field(:type, :string, primary_key: true)
    field(:table_name, :string, primary_key: true, source: :tableName)
    field(:channel_id, :string, primary_key: true, source: :channelId)
    field(:flapping_since, Jitsu.PrismaDateTime, source: :flappingSince)
    field(:last_notification, Jitsu.PrismaDateTime, source: :lastNotification)
    field(:status_change_id, :integer, source: :statusChangeId)
    field(:error, :string)
  end
end