defmodule Umami.ApiKey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Umami.PrismaDateTime, autogenerate: {Umami.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          key_hash: String.t(),
          key_prefix: String.t(),
          last_used_at: DateTime.t() | nil,
          inserted_at: DateTime.t() | nil,
          user: Umami.User.t() | nil
        }

  schema "api_key" do
    field(:id, :string, primary_key: true, source: :api_key_id)
    field(:name, :string)
    field(:key_hash, :string)
    field(:key_prefix, :string)
    field(:last_used_at, Umami.PrismaDateTime)
    belongs_to(:user, Umami.User, foreign_key: :user_id, type: :string)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end