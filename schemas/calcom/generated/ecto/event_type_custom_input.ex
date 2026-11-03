defmodule Calcom.EventTypeCustomInput do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          label: String.t(),
          type: atom(),
          options: map() | nil,
          required: boolean(),
          placeholder: String.t(),
          event_type: Calcom.EventType.t() | nil
        }

  schema "EventTypeCustomInput" do
    field(:label, :string)
    field(:type, Ecto.Enum, values: [TEXT: "text", TEXTLONG: "textLong", NUMBER: "number", BOOL: "bool", RADIO: "radio", PHONE: "phone"])
    field(:options, :map)
    field(:required, :boolean)
    field(:placeholder, :string, default: "")
    field(:event_type_id, :id, source: :eventTypeId)
    belongs_to(:event_type, Calcom.EventType, foreign_key: :event_type_id, define_field: false)
  end
end