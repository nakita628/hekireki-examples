defmodule Ghostfolio.Tag do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          user: Ghostfolio.User.t() | nil,
          accounts: [Ghostfolio.TagsOnAccounts.t()],
          activities: [Ghostfolio.Order.t()]
        }

  schema "Tag" do
    field(:name, :string)
    field(:user_id, :binary_id, source: :userId)
    belongs_to(:user, Ghostfolio.User, foreign_key: :user_id, define_field: false)
    has_many(:accounts, Ghostfolio.TagsOnAccounts, foreign_key: :tag_id)
    many_to_many(:activities, Ghostfolio.Order, join_through: "_OrderToTag", join_keys: [B: :id, A: :id])
  end
end