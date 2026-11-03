defmodule Calcom.HostGroup do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          event_type: Calcom.EventType.t() | nil,
          hosts: [Calcom.Host.t()]
        }

  schema "HostGroup" do
    field(:name, :string)
    field(:event_type_id, :id, source: :eventTypeId)
    belongs_to(:event_type, Calcom.EventType, foreign_key: :event_type_id, define_field: false, type: :id)
    has_many(:hosts, Calcom.Host, foreign_key: :group_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end