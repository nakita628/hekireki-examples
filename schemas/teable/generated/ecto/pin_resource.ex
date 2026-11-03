defmodule Teable.PinResource do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t(),
          resource_id: String.t(),
          created_time: DateTime.t(),
          created_by: String.t(),
          order: float()
        }

  schema "pin_resource" do
    field(:id, :string, primary_key: true)
    field(:type, :string)
    field(:resource_id, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    field(:order, :float)
  end
end