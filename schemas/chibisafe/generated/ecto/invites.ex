defmodule Chibisafe.Invites do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Chibisafe.PrismaDateTime, autogenerate: {Chibisafe.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          created_by: String.t(),
          used_by: String.t() | nil,
          code: String.t(),
          used: boolean(),
          edited_at: DateTime.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "invites" do
    field(:created_by, :string, source: :createdBy)
    field(:used_by, :string, source: :usedBy)
    field(:code, :string)
    field(:used, :boolean, default: false)
    field(:edited_at, Chibisafe.PrismaDateTime, source: :editedAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end