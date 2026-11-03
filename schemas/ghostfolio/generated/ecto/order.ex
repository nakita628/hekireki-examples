defmodule Ghostfolio.Order do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Ghostfolio.PrismaDateTime, autogenerate: {Ghostfolio.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          account_id: String.t() | nil,
          account_user_id: String.t() | nil,
          comment: String.t() | nil,
          currency: String.t() | nil,
          date: DateTime.t(),
          fee: float(),
          quantity: float(),
          type: atom(),
          unit_price: float(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Ghostfolio.User.t() | nil,
          symbol_profile: Ghostfolio.SymbolProfile.t() | nil,
          tags: [Ghostfolio.Tag.t()]
        }

  schema "Order" do
    field(:account_id, :string, source: :accountId)
    field(:account_user_id, :string, source: :accountUserId)
    field(:comment, :string)
    field(:currency, :string)
    field(:date, Ghostfolio.PrismaDateTime)
    field(:fee, :float)
    field(:quantity, :float)
    field(:type, Ecto.Enum, values: [:BUY, :DIVIDEND, :FEE, :INTEREST, :LIABILITY, :SELL])
    field(:unit_price, :float, source: :unitPrice)
    field(:user_id, :binary_id, source: :userId)
    field(:symbol_profile_id, :binary_id, source: :symbolProfileId)
    belongs_to(:user, Ghostfolio.User, foreign_key: :user_id, define_field: false)
    belongs_to(:symbol_profile, Ghostfolio.SymbolProfile, foreign_key: :symbol_profile_id, define_field: false)
    many_to_many(:tags, Ghostfolio.Tag, join_through: "_OrderToTag", join_keys: [A: :id, B: :id])
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end