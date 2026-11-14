defmodule LangWatch.VirtualKeyScope do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          scope_type: atom(),
          scope_id: String.t(),
          inserted_at: DateTime.t(),
          virtual_key: LangWatch.VirtualKey.t() | nil
        }

  schema "VirtualKeyScope" do
    field(:id, :string, primary_key: true)
    field(:scope_type, Ecto.Enum, values: [:ORGANIZATION, :TEAM, :PROJECT], source: :scopeType)
    field(:scope_id, :string, source: :scopeId)
    field(:virtual_key_id, :string, source: :virtualKeyId)
    belongs_to(:virtual_key, LangWatch.VirtualKey, foreign_key: :virtual_key_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end