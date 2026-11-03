defmodule Typebot.ApiToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          token: String.t(),
          name: String.t(),
          inserted_at: DateTime.t(),
          owner: Typebot.User.t() | nil
        }

  schema "ApiToken" do
    field(:id, :string, primary_key: true)
    field(:token, :string)
    field(:name, :string)
    field(:owner_id, :string, source: :ownerId)
    belongs_to(:owner, Typebot.User, foreign_key: :owner_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end