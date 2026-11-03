defmodule Teable.ComputedTaskFieldRef do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          task_id: String.t(),
          field_id: String.t(),
          table_id: String.t(),
          base_id: String.t(),
          was_processing: boolean(),
          inserted_at: DateTime.t()
        }

  schema "computed_task_field_ref" do
    field(:task_id, :string, primary_key: true)
    field(:field_id, :string, primary_key: true)
    field(:table_id, :string)
    field(:base_id, :string)
    field(:was_processing, :boolean, default: false)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end