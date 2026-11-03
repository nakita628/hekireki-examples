defmodule Teable.SchemaOperation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: String.t(),
          status: String.t(),
          phase: String.t(),
          resource_type: String.t(),
          resource_id: String.t(),
          base_id: String.t() | nil,
          table_id: String.t() | nil,
          idempotency_key: String.t(),
          payload: map() | nil,
          result: map() | nil,
          attempts: integer(),
          max_attempts: integer(),
          next_run_at: DateTime.t(),
          locked_at: DateTime.t() | nil,
          locked_by: String.t() | nil,
          last_error: String.t() | nil,
          created_time: DateTime.t(),
          created_by: String.t(),
          last_modified_by: String.t() | nil,
          updated_at: DateTime.t() | nil
        }

  schema "schema_operation" do
    field(:id, :string, primary_key: true)
    field(:type, :string)
    field(:status, :string)
    field(:phase, :string)
    field(:resource_type, :string)
    field(:resource_id, :string)
    field(:base_id, :string)
    field(:table_id, :string)
    field(:idempotency_key, :string)
    field(:payload, :map)
    field(:result, :map)
    field(:attempts, :integer, default: 0)
    field(:max_attempts, :integer, default: 8)
    field(:next_run_at, Teable.PrismaDateTime, autogenerate: true)
    field(:locked_at, Teable.PrismaDateTime)
    field(:locked_by, :string)
    field(:last_error, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    field(:last_modified_by, :string)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end