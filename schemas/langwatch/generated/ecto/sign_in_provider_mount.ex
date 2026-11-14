defmodule LangWatch.SignInProviderMount do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          provider: String.t() | nil,
          mounted: boolean(),
          last_applied_event_id: String.t(),
          updated_at: DateTime.t()
        }

  schema "SignInProviderMount" do
    field(:id, :string, primary_key: true, default: "self")
    field(:provider, :string)
    field(:mounted, :boolean)
    field(:last_applied_event_id, :string, source: :lastAppliedEventId)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end