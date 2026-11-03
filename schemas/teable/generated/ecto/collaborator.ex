defmodule Teable.Collaborator do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          role_name: String.t(),
          resource_type: String.t(),
          resource_id: String.t(),
          principal_id: String.t(),
          principal_type: String.t(),
          created_by: String.t(),
          created_time: DateTime.t(),
          last_modified_by: String.t() | nil,
          updated_at: DateTime.t() | nil
        }

  schema "collaborator" do
    field(:id, :string, primary_key: true)
    field(:role_name, :string)
    field(:resource_type, :string)
    field(:resource_id, :string)
    field(:principal_id, :string)
    field(:principal_type, :string)
    field(:created_by, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:last_modified_by, :string)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end