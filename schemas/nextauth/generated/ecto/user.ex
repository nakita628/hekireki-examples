defmodule NextAuth.User do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t() | nil,
          email: String.t(),
          email_verified: DateTime.t() | nil,
          image: String.t() | nil,
          accounts: [NextAuth.Account.t()],
          sessions: [NextAuth.Session.t()],
          authenticator: [NextAuth.Authenticator.t()]
        }

  schema "User" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:email, :string)
    field(:email_verified, NextAuth.PrismaDateTime, source: :emailVerified)
    field(:image, :string)
    has_many(:accounts, NextAuth.Account, foreign_key: :user_id, references: :id)
    has_many(:sessions, NextAuth.Session, foreign_key: :user_id, references: :id)
    has_many(:authenticator, NextAuth.Authenticator, foreign_key: :user_id, references: :id)
  end
end