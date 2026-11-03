defmodule Calcom.Payment do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          uid: String.t(),
          amount: integer(),
          fee: integer(),
          currency: String.t(),
          success: boolean(),
          refunded: boolean(),
          data: map(),
          external_id: String.t(),
          payment_option: atom() | nil,
          app: Calcom.App.t() | nil,
          booking: Calcom.Booking.t() | nil
        }

  schema "Payment" do
    field(:uid, :string)
    field(:amount, :integer)
    field(:fee, :integer)
    field(:currency, :string)
    field(:success, :boolean)
    field(:refunded, :boolean)
    field(:data, :map)
    field(:external_id, :string, source: :externalId)
    field(:payment_option, Ecto.Enum, values: [:ON_BOOKING, :HOLD], default: :ON_BOOKING, source: :paymentOption)
    field(:app_id, :string, source: :appId)
    field(:booking_id, :id, source: :bookingId)
    belongs_to(:app, Calcom.App, foreign_key: :app_id, define_field: false, type: :string, references: :slug)
    belongs_to(:booking, Calcom.Booking, foreign_key: :booking_id, define_field: false)
  end
end