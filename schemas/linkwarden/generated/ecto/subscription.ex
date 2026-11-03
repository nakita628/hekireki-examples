defmodule Linkwarden.Subscription do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Linkwarden.PrismaDateTime, autogenerate: {Linkwarden.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          active: boolean(),
          provider: atom(),
          current_period_start: DateTime.t(),
          current_period_end: DateTime.t(),
          quantity: integer(),
          store_original_transaction_id: String.t() | nil,
          store_product_id: String.t() | nil,
          google_purchase_token: String.t() | nil,
          store_metadata: map() | nil,
          stripe_subscription_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Linkwarden.User.t() | nil,
          child_users: [Linkwarden.User.t()]
        }

  schema "Subscription" do
    field(:active, :boolean)
    field(:provider, Ecto.Enum, values: [:STRIPE, :APPLE, :GOOGLE])
    field(:current_period_start, Linkwarden.PrismaDateTime, source: :currentPeriodStart)
    field(:current_period_end, Linkwarden.PrismaDateTime, source: :currentPeriodEnd)
    field(:quantity, :integer, default: 1)
    field(:store_original_transaction_id, :string, source: :storeOriginalTransactionId)
    field(:store_product_id, :string, source: :storeProductId)
    field(:google_purchase_token, :string, source: :googlePurchaseToken)
    field(:store_metadata, :map, source: :storeMetadata)
    field(:stripe_subscription_id, :string, source: :stripeSubscriptionId)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Linkwarden.User, foreign_key: :user_id, define_field: false)
    has_many(:child_users, Linkwarden.User, foreign_key: :parent_subscription_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end