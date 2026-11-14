defmodule LangWatch.GatewayBudgetBucketBoundary do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          bucket_scope_id: String.t(),
          period_started_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          budget: LangWatch.GatewayBudget.t() | nil
        }

  schema "GatewayBudgetBucketBoundary" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:bucket_scope_id, :string, source: :bucketScopeId)
    field(:period_started_at, LangWatch.PrismaDateTime, source: :periodStartedAt)
    field(:budget_id, :string, source: :budgetId)
    belongs_to(:budget, LangWatch.GatewayBudget, foreign_key: :budget_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end