defmodule Calcom.SecondaryEmail do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          email: String.t(),
          email_verified: DateTime.t() | nil,
          user: Calcom.User.t() | nil,
          verification_tokens: [Calcom.VerificationToken.t()],
          event_types: [Calcom.EventType.t()]
        }

  schema "SecondaryEmail" do
    field(:email, :string)
    field(:email_verified, Calcom.PrismaDateTime, source: :emailVerified)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    has_many(:verification_tokens, Calcom.VerificationToken, foreign_key: :secondary_email_id)
    has_many(:event_types, Calcom.EventType, foreign_key: :secondary_email_id)
  end
end