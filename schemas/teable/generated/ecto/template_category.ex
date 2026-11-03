defmodule Teable.TemplateCategory do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          created_time: DateTime.t(),
          created_by: String.t(),
          last_modified_by: String.t() | nil,
          order: float(),
          updated_at: DateTime.t() | nil
        }

  schema "template_category" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    field(:last_modified_by, :string)
    field(:order, :float)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end