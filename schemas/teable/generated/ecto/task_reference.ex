defmodule Teable.TaskReference do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          from_field_id: String.t(),
          to_field_id: String.t(),
          created_time: DateTime.t()
        }

  schema "task_reference" do
    field(:id, :string, primary_key: true)
    field(:from_field_id, :string)
    field(:to_field_id, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
  end
end