defmodule Dub.Token do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          hashed_key: String.t(),
          partial_key: String.t(),
          expires: DateTime.t() | nil,
          last_used: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Dub.User.t() | nil
        }

  schema "Token" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:hashed_key, :string, source: :hashedKey)
    field(:partial_key, :string, source: :partialKey)
    field(:expires, Dub.PrismaDateTime)
    field(:last_used, Dub.PrismaDateTime, source: :lastUsed)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, Dub.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end