defmodule Calcom.Attendee do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          email: String.t(),
          name: String.t(),
          time_zone: String.t(),
          phone_number: String.t() | nil,
          locale: String.t() | nil,
          no_show: boolean() | nil,
          booking: Calcom.Booking.t() | nil,
          booking_seat: Calcom.BookingSeat.t() | nil
        }

  schema "Attendee" do
    field(:email, :string)
    field(:name, :string)
    field(:time_zone, :string, source: :timeZone)
    field(:phone_number, :string, source: :phoneNumber)
    field(:locale, :string, default: "en")
    field(:no_show, :boolean, default: false, source: :noShow)
    field(:booking_id, :id, source: :bookingId)
    belongs_to(:booking, Calcom.Booking, foreign_key: :booking_id, define_field: false)
    has_one(:booking_seat, Calcom.BookingSeat, foreign_key: :attendee_id)
  end
end