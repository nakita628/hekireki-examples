defmodule LangWatch.DiscoveredAgent do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          provider: String.t(),
          raw_agent_id: String.t(),
          display_text: String.t(),
          metadata: map(),
          first_seen_at: DateTime.t(),
          last_seen_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "DiscoveredAgent" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:provider, :string)
    field(:raw_agent_id, :string, source: :rawAgentId)
    field(:display_text, :string, source: :displayText)
    field(:metadata, :map)
    field(:first_seen_at, LangWatch.PrismaDateTime, source: :firstSeenAt)
    field(:last_seen_at, LangWatch.PrismaDateTime, source: :lastSeenAt)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end