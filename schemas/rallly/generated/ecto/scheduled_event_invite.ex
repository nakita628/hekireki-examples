defmodule Rallly.ScheduledEventInvite do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          uid: String.t(),
          invitee_name: String.t(),
          invitee_email: String.t(),
          invitee_time_zone: String.t() | nil,
          invitee_locale: String.t() | nil,
          status: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          scheduled_event: Rallly.ScheduledEvent.t() | nil,
          user: Rallly.User.t() | nil
        }

  schema "scheduled_event_invites" do
    field(:id, :string, primary_key: true)
    field(:uid, :string)
    field(:invitee_name, :string)
    field(:invitee_email, :string)
    field(:invitee_time_zone, :string)
    field(:invitee_locale, :string)
    field(:status, Ecto.Enum, values: [:pending, :accepted, :declined, :tentative], default: :pending)
    belongs_to(:scheduled_event, Rallly.ScheduledEvent, foreign_key: :scheduled_event_id, type: :string)
    belongs_to(:user, Rallly.User, foreign_key: :invitee_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end