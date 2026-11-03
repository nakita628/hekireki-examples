defmodule Calcom.BookingDenormalized do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: integer(),
          uid: String.t(),
          event_type_id: integer() | nil,
          title: String.t(),
          description: String.t() | nil,
          start_time: DateTime.t(),
          end_time: DateTime.t(),
          created_at: DateTime.t(),
          updated_at: DateTime.t() | nil,
          location: String.t() | nil,
          paid: boolean(),
          status: atom(),
          rescheduled: boolean() | nil,
          user_id: integer() | nil,
          team_id: integer() | nil,
          event_length: integer() | nil,
          event_parent_id: integer() | nil,
          user_email: String.t() | nil,
          user_name: String.t() | nil,
          user_username: String.t() | nil,
          rating_feedback: String.t() | nil,
          rating: integer() | nil,
          no_show_host: boolean() | nil,
          is_team_booking: boolean()
        }

  schema "BookingDenormalized" do
    field(:id, :integer, primary_key: true)
    field(:uid, :string)
    field(:event_type_id, :integer, source: :eventTypeId)
    field(:title, :string)
    field(:description, :string)
    field(:start_time, Calcom.PrismaDateTime, source: :startTime)
    field(:end_time, Calcom.PrismaDateTime, source: :endTime)
    field(:created_at, Calcom.PrismaDateTime, source: :createdAt)
    field(:updated_at, Calcom.PrismaDateTime, source: :updatedAt)
    field(:location, :string)
    field(:paid, :boolean)
    field(:status, Ecto.Enum, values: [CANCELLED: "cancelled", ACCEPTED: "accepted", REJECTED: "rejected", PENDING: "pending", AWAITING_HOST: "awaiting_host"])
    field(:rescheduled, :boolean)
    field(:user_id, :integer, source: :userId)
    field(:team_id, :integer, source: :teamId)
    field(:event_length, :integer, source: :eventLength)
    field(:event_parent_id, :integer, source: :eventParentId)
    field(:user_email, :string, source: :userEmail)
    field(:user_name, :string, source: :userName)
    field(:user_username, :string, source: :userUsername)
    field(:rating_feedback, :string, source: :ratingFeedback)
    field(:rating, :integer)
    field(:no_show_host, :boolean, source: :noShowHost)
    field(:is_team_booking, :boolean, source: :isTeamBooking)
  end
end