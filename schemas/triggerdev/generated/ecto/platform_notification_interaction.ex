defmodule Trigger.PlatformNotificationInteraction do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          first_seen_at: DateTime.t(),
          show_count: integer(),
          webapp_dismissed_at: DateTime.t() | nil,
          webapp_clicked_at: DateTime.t() | nil,
          cli_dismissed_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          notification: Trigger.PlatformNotification.t() | nil,
          user: Trigger.User.t() | nil
        }

  schema "PlatformNotificationInteraction" do
    field(:id, :string, primary_key: true)
    field(:first_seen_at, Trigger.PrismaDateTime, autogenerate: true, source: :firstSeenAt)
    field(:show_count, :integer, default: 1, source: :showCount)
    field(:webapp_dismissed_at, Trigger.PrismaDateTime, source: :webappDismissedAt)
    field(:webapp_clicked_at, Trigger.PrismaDateTime, source: :webappClickedAt)
    field(:cli_dismissed_at, Trigger.PrismaDateTime, source: :cliDismissedAt)
    field(:notification_id, :string, source: :notificationId)
    field(:user_id, :string, source: :userId)
    belongs_to(:notification, Trigger.PlatformNotification, foreign_key: :notification_id, define_field: false, type: :string)
    belongs_to(:user, Trigger.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end