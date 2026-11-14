defmodule LangWatch.DiscoveredPerson do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          provider: String.t(),
          raw_actor_id: String.t(),
          display_text: String.t(),
          kind: String.t(),
          department: String.t() | nil,
          first_seen_at: DateTime.t(),
          last_seen_at: DateTime.t(),
          erased_at: DateTime.t() | nil,
          money_rows_pending_at: DateTime.t() | nil,
          money_rebuild_since: String.t() | nil,
          suspended_at: DateTime.t() | nil,
          suspended_reason: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "DiscoveredPerson" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:provider, :string)
    field(:raw_actor_id, :string, source: :rawActorId)
    field(:display_text, :string, source: :displayText)
    field(:kind, :string)
    field(:department, :string)
    field(:first_seen_at, LangWatch.PrismaDateTime, source: :firstSeenAt)
    field(:last_seen_at, LangWatch.PrismaDateTime, source: :lastSeenAt)
    field(:erased_at, LangWatch.PrismaDateTime, source: :erasedAt)
    field(:money_rows_pending_at, LangWatch.PrismaDateTime, source: :moneyRowsPendingAt)
    field(:money_rebuild_since, :string, source: :moneyRebuildSince)
    field(:suspended_at, LangWatch.PrismaDateTime, source: :suspendedAt)
    field(:suspended_reason, :string, source: :suspendedReason)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end