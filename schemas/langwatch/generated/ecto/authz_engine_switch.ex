defmodule LangWatch.AuthzEngineSwitch do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          organization_id: String.t(),
          on_engine: boolean(),
          pinned: boolean(),
          switched_at: DateTime.t(),
          last_applied_event_id: String.t() | nil,
          updated_at: DateTime.t()
        }

  schema "AuthzEngineSwitch" do
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    field(:on_engine, :boolean, source: :onEngine)
    field(:pinned, :boolean, default: false)
    field(:switched_at, LangWatch.PrismaDateTime, source: :switchedAt)
    field(:last_applied_event_id, :string, source: :lastAppliedEventId)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end