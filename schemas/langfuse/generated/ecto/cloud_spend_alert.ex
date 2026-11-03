defmodule Langfuse.CloudSpendAlert do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          title: String.t(),
          threshold: Decimal.t(),
          triggered_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          org: Langfuse.Organization.t() | nil
        }

  schema "cloud_spend_alerts" do
    field(:id, :string, primary_key: true)
    field(:title, :string)
    field(:threshold, :decimal)
    field(:triggered_at, Langfuse.PrismaDateTime)
    belongs_to(:org, Langfuse.Organization, foreign_key: :org_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end