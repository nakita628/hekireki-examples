defmodule Calcom.CreditExpenseLog do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          credits: integer() | nil,
          credit_type: atom(),
          date: DateTime.t(),
          sms_sid: String.t() | nil,
          sms_segments: integer() | nil,
          phone_number: String.t() | nil,
          email: String.t() | nil,
          call_duration: integer() | nil,
          credit_for: atom() | nil,
          external_ref: String.t() | nil,
          credit_balance: Calcom.CreditBalance.t() | nil,
          booking: Calcom.Booking.t() | nil
        }

  schema "CreditExpenseLog" do
    field(:credits, :integer)
    field(:credit_type, Ecto.Enum, values: [:MONTHLY, :ADDITIONAL], source: :creditType)
    field(:date, Calcom.PrismaDateTime)
    field(:sms_sid, :string, source: :smsSid)
    field(:sms_segments, :integer, source: :smsSegments)
    field(:phone_number, :string, source: :phoneNumber)
    field(:email, :string)
    field(:call_duration, :integer, source: :callDuration)
    field(:credit_for, Ecto.Enum, values: [:SMS, :CAL_AI_PHONE_CALL], source: :creditFor)
    field(:external_ref, :string, source: :externalRef)
    field(:credit_balance_id, :binary_id, source: :creditBalanceId)
    field(:booking_uid, :string, source: :bookingUid)
    belongs_to(:credit_balance, Calcom.CreditBalance, foreign_key: :credit_balance_id, define_field: false)
    belongs_to(:booking, Calcom.Booking, foreign_key: :booking_uid, define_field: false, type: :string, references: :uid)
  end
end