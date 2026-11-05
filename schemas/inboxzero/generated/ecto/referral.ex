defmodule InboxZero.Referral do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          referral_code_used: String.t(),
          status: atom(),
          reward_granted_at: DateTime.t() | nil,
          stripe_balance_transaction_id: String.t() | nil,
          reward_amount: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          referrer_user: InboxZero.User.t() | nil,
          referred_user: InboxZero.User.t() | nil
        }

  schema "Referral" do
    field(:id, :string, primary_key: true)
    field(:referral_code_used, :string, source: :referralCodeUsed)
    field(:status, Ecto.Enum, values: [:PENDING, :COMPLETED], default: :PENDING)
    field(:reward_granted_at, InboxZero.PrismaDateTime, source: :rewardGrantedAt)
    field(:stripe_balance_transaction_id, :string, source: :stripeBalanceTransactionId)
    field(:reward_amount, :integer, source: :rewardAmount)
    field(:referrer_user_id, :string, source: :referrerUserId)
    field(:referred_user_id, :string, source: :referredUserId)
    belongs_to(:referrer_user, InboxZero.User, foreign_key: :referrer_user_id, define_field: false, type: :string)
    belongs_to(:referred_user, InboxZero.User, foreign_key: :referred_user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end