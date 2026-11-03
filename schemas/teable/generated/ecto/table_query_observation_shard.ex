defmodule Teable.TableQueryObservationShard do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          space_id: String.t() | nil,
          base_id: String.t(),
          table_id: String.t(),
          query_kind: String.t(),
          shape_hash: String.t(),
          window_start: DateTime.t(),
          writer_id: String.t(),
          window_size_seconds: integer(),
          request_count: integer(),
          slow_count: integer(),
          timeout_count: integer(),
          db_error_count: integer(),
          total_duration_ms: float(),
          max_duration_ms: float(),
          total_db_duration_ms: float() | nil,
          max_db_duration_ms: float() | nil,
          shape: map(),
          sql_diagnostics: map() | nil,
          created_time: DateTime.t(),
          last_modified_time: DateTime.t() | nil
        }

  schema "table_query_observation_shard" do
    field(:space_id, :string)
    field(:base_id, :string)
    field(:table_id, :string, primary_key: true)
    field(:query_kind, :string, primary_key: true)
    field(:shape_hash, :string, primary_key: true)
    field(:window_start, Teable.PrismaDateTime, primary_key: true)
    field(:writer_id, :string, primary_key: true)
    field(:window_size_seconds, :integer)
    field(:request_count, :integer)
    field(:slow_count, :integer)
    field(:timeout_count, :integer)
    field(:db_error_count, :integer)
    field(:total_duration_ms, :float)
    field(:max_duration_ms, :float)
    field(:total_db_duration_ms, :float)
    field(:max_db_duration_ms, :float)
    field(:shape, :map)
    field(:sql_diagnostics, :map)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:last_modified_time, Teable.PrismaDateTime)
  end
end