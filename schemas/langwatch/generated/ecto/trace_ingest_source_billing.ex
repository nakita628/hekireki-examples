defmodule LangWatch.TraceIngestSourceBilling do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          organization_id: String.t(),
          source_type: String.t(),
          billed: boolean(),
          recorded_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "TraceIngestSourceBilling" do
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    field(:source_type, :string, primary_key: true, source: :sourceType)
    field(:billed, :boolean)
    field(:recorded_at, LangWatch.PrismaDateTime, source: :recordedAt)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end