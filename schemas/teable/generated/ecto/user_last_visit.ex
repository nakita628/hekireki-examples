defmodule Teable.UserLastVisit do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          user_id: String.t(),
          resource_type: String.t(),
          resource_id: String.t(),
          parent_resource_id: String.t(),
          last_visit_time: DateTime.t()
        }

  schema "user_last_visit" do
    field(:id, :string, primary_key: true)
    field(:user_id, :string)
    field(:resource_type, :string)
    field(:resource_id, :string)
    field(:parent_resource_id, :string)
    field(:last_visit_time, Teable.PrismaDateTime, autogenerate: true)
  end
end