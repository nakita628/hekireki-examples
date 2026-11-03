defmodule Ghostfolio.ApiKey do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Ghostfolio.PrismaDateTime, autogenerate: {Ghostfolio.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          hashed_key: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Ghostfolio.User.t() | nil
        }

  schema "ApiKey" do
    field(:hashed_key, :string, source: :hashedKey)
    field(:user_id, :binary_id, source: :userId)
    belongs_to(:user, Ghostfolio.User, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end