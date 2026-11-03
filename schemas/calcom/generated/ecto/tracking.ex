defmodule Calcom.Tracking do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          utm_source: String.t() | nil,
          utm_medium: String.t() | nil,
          utm_campaign: String.t() | nil,
          utm_term: String.t() | nil,
          utm_content: String.t() | nil,
          booking: Calcom.Booking.t() | nil
        }

  schema "Tracking" do
    field(:utm_source, :string)
    field(:utm_medium, :string)
    field(:utm_campaign, :string)
    field(:utm_term, :string)
    field(:utm_content, :string)
    field(:booking_id, :id, source: :bookingId)
    belongs_to(:booking, Calcom.Booking, foreign_key: :booking_id, define_field: false)
  end
end