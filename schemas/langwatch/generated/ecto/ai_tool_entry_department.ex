defmodule LangWatch.AiToolEntryDepartment do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          department_id: String.t(),
          entry: LangWatch.AiToolEntry.t() | nil
        }

  schema "AiToolEntryDepartment" do
    field(:id, :string, primary_key: true)
    field(:department_id, :string, source: :departmentId)
    field(:entry_id, :string, source: :entryId)
    belongs_to(:entry, LangWatch.AiToolEntry, foreign_key: :entry_id, define_field: false, type: :string)
  end
end