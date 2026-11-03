defmodule Ghostfolio.Access do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Ghostfolio.PrismaDateTime, autogenerate: {Ghostfolio.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          alias: String.t() | nil,
          expires_at: DateTime.t(),
          last_used_at: DateTime.t() | nil,
          scopes: [String.t()],
          settings: map(),
          type: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          grantee_user: Ghostfolio.User.t() | nil,
          user: Ghostfolio.User.t() | nil
        }

  schema "Access" do
    field(:alias, :string)
    field(:expires_at, Ghostfolio.PrismaDateTime, source: :expiresAt)
    field(:last_used_at, Ghostfolio.PrismaDateTime, source: :lastUsedAt)
    field(:scopes, {:array, :string})
    field(:settings, :map, default: %{})
    field(:type, Ecto.Enum, values: [:MCP, :PRIVATE, :PUBLIC], default: :PRIVATE)
    field(:grantee_user_id, :binary_id, source: :granteeUserId)
    field(:user_id, :binary_id, source: :userId)
    belongs_to(:grantee_user, Ghostfolio.User, foreign_key: :grantee_user_id, define_field: false)
    belongs_to(:user, Ghostfolio.User, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end