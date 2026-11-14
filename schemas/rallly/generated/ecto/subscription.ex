defmodule Rallly.Subscription do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          price_id: String.t(),
          quantity: integer(),
          subscription_item_id: String.t(),
          amount: integer(),
          discount_percent_off: float() | nil,
          discount_amount_off: integer() | nil,
          status: atom(),
          active: boolean(),
          currency: String.t(),
          interval: atom(),
          period_start: DateTime.t(),
          period_end: DateTime.t(),
          cancel_at_period_end: boolean(),
          api_addon_item_id: String.t() | nil,
          api_addon_price_id: String.t() | nil,
          api_addon_amount: integer() | nil,
          api_addon_tier: String.t() | nil,
          api_poll_retained_tier: String.t() | nil,
          api_poll_retained_until: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          user: Rallly.User.t() | nil,
          space: Rallly.Space.t() | nil
        }

  schema "subscriptions" do
    field(:id, :string, primary_key: true)
    field(:price_id, :string)
    field(:quantity, :integer, default: 1)
    field(:subscription_item_id, :string)
    field(:amount, :integer)
    field(:discount_percent_off, :float)
    field(:discount_amount_off, :integer)
    field(:status, Ecto.Enum, values: [:incomplete, :incomplete_expired, :active, :paused, :trialing, :past_due, :canceled, :unpaid])
    field(:active, :boolean)
    field(:currency, :string)
    field(:interval, Ecto.Enum, values: [:month, :year])
    field(:period_start, Rallly.PrismaDateTime)
    field(:period_end, Rallly.PrismaDateTime)
    field(:cancel_at_period_end, :boolean, default: false)
    field(:api_addon_item_id, :string)
    field(:api_addon_price_id, :string)
    field(:api_addon_amount, :integer)
    field(:api_addon_tier, :string)
    field(:api_poll_retained_tier, :string)
    field(:api_poll_retained_until, Rallly.PrismaDateTime)
    belongs_to(:user, Rallly.User, foreign_key: :user_id, type: :string)
    belongs_to(:space, Rallly.Space, foreign_key: :space_id, type: :binary_id)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end