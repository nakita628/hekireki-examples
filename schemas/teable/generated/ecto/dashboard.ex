defmodule Teable.Dashboard do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          base_id: String.t(),
          layout: String.t() | nil,
          created_by: String.t(),
          created_time: DateTime.t(),
          last_modified_by: String.t() | nil,
          updated_at: DateTime.t() | nil
        }

  schema "dashboard" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:base_id, :string)
    field(:layout, :string)
    field(:created_by, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:last_modified_by, :string)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end