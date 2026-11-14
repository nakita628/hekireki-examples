defmodule LangWatch.IdentityProjectionCursor do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          user_id: String.t(),
          last_event_id: String.t(),
          accepted_at: DateTime.t(),
          occurred_at: DateTime.t(),
          projection_version: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "IdentityProjectionCursor" do
    field(:user_id, :string, primary_key: true, source: :userId)
    field(:last_event_id, :string, source: :lastEventId)
    field(:accepted_at, LangWatch.PrismaDateTime, source: :acceptedAt)
    field(:occurred_at, LangWatch.PrismaDateTime, source: :occurredAt)
    field(:projection_version, :string, source: :projectionVersion)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end