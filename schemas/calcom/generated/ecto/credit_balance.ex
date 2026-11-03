defmodule Calcom.CreditBalance do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          additional_credits: integer(),
          limit_reached_at: DateTime.t() | nil,
          warning_sent_at: DateTime.t() | nil,
          team: Calcom.Team.t() | nil,
          user: Calcom.User.t() | nil,
          expense_logs: [Calcom.CreditExpenseLog.t()],
          purchase_logs: [Calcom.CreditPurchaseLog.t()]
        }

  schema "CreditBalance" do
    field(:additional_credits, :integer, default: 0, source: :additionalCredits)
    field(:limit_reached_at, Calcom.PrismaDateTime, source: :limitReachedAt)
    field(:warning_sent_at, Calcom.PrismaDateTime, source: :warningSentAt)
    field(:team_id, :id, source: :teamId)
    field(:user_id, :id, source: :userId)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false, type: :id)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false, type: :id)
    has_many(:expense_logs, Calcom.CreditExpenseLog, foreign_key: :credit_balance_id)
    has_many(:purchase_logs, Calcom.CreditPurchaseLog, foreign_key: :credit_balance_id)
  end
end