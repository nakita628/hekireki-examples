defmodule LangWatch.SlackConnectionClaim do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          connection_id: String.t(),
          claimant_id: String.t(),
          claimant_label: String.t(),
          organization_id: String.t(),
          project_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "slack_connection_claim" do
    field(:connection_id, :string, primary_key: true, source: :connectionId)
    field(:claimant_id, :string, primary_key: true, source: :claimantId)
    field(:claimant_label, :string, source: :claimantLabel)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end