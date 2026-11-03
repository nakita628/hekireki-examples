defmodule Umami.TwoFactorOtpUsed do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          otp: String.t(),
          expires_at: DateTime.t(),
          user: Umami.User.t() | nil
        }

  schema "two_factor_otp_used" do
    field(:id, :string, primary_key: true)
    field(:otp, :string)
    field(:expires_at, Umami.PrismaDateTime)
    belongs_to(:user, Umami.User, foreign_key: :user_id, type: :string)
  end
end